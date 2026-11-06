using Ensinly.Server.DAO.Interfaces;
using Ensinly.Server.DTO;
using Ensinly.Server.Facade.Inteface;
using Ensinly.Server.Models;
using Ensinly.Server.Service;

namespace Ensinly.Server.Facade {
    public class FacadeConvite : IFacadeConvite {
        private readonly IDAOProfessor _daoProfessor;
        private readonly IDAOAluno _daoAluno;
        private readonly IDAOConvite _daoConvite;
        private readonly ConviteService _conviteService;
        private readonly EmailService _emailService;


        public FacadeConvite(IDAOProfessor daoProfessor, IDAOAluno daoAluno, IDAOConvite daoConvite, ConviteService conviteService, EmailService emailService) {
            _daoProfessor = daoProfessor;
            _daoAluno = daoAluno;
            _daoConvite = daoConvite;
            _conviteService = conviteService;
            _emailService = emailService;
        }

        public async Task<Convite> CriarConvite(ConviteRequestDTO conviteDto) {
            var Convite = new Convite {
                Professor = await _daoProfessor.SelectProfessorByIdUsuario(conviteDto.IdProfessorUsuario),
                Aluno = await _daoAluno.SelectByIdAluno(conviteDto.IdAluno),
                DataCriacao = DateTime.Now,
                DataExpiracao = DateTime.Now.AddDays(1),
                TokenHash = _conviteService.GenerateToken()
            };

            if (!verificarEmailAluno(Convite.Aluno, conviteDto.EmailDestinatario)) {
                throw new Exception("O email do destinatário não corresponde ao aluno selecionado.");
            }

            try {
                await _daoConvite.InsertConvite(Convite);
                await EnviarConvite(Convite);
            } catch (Exception ex) {
                throw new Exception($"Erro ao criar convite: {ex.Message}", ex);
            }

            return Convite;
        }

        public async Task EnviarConvite(Convite convite) {
            string assunto = "Você recebeu um convite para se vincular ao Ensinly!";
            string link = _conviteService.GenerateConviteLink(convite.TokenHash);
            string mensagem = $@"
                <html>
                    <body style='font-family: Arial, sans-serif; color: #333;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px;'>

                            <h2 style='color: #2563eb;'>
                                Olá!
                            </h2>

                            <p>
                                Você recebeu um convite de um professor para se
                                vincular à plataforma <strong>Ensinly</strong>.
                            </p>

                            <p>
                                Com esse vínculo, você poderá acompanhar suas
                                atividades e seu aprendizado de forma organizada
                                e personalizada.
                            </p>

                            <p>
                                Para aceitar o convite e concluir o vínculo,
                                basta clicar no botão abaixo:
                            </p>

                            <div style='text-align: center; margin: 30px 0;'>
                                <a href='{link}'
                                   style='
                                       background-color: #2563eb;
                                       color: #ffffff;
                                       padding: 14px 28px;
                                       text-decoration: none;
                                       border-radius: 8px;
                                       font-weight: bold;
                                       display: inline-block;
                                   '>
                                    Aceitar Convite
                                </a>
                            </div>

                            <p>
                                Este convite é válido por 1 dia e poderá ser
                                utilizado apenas uma vez.
                            </p>

                            <p>
                                Caso você não reconheça este convite,
                                basta ignorar este email.
                            </p>

                            <hr style='border: 0; border-top: 1px solid #ddd;' />

                            <p style='font-size: 12px; color: #777;'>
                                Atenciosamente,<br />
                                <strong>Equipe Ensinly</strong><br />
                                Seu aprendizado, nossa missão.
                            </p>

                        </div>
                    </body>
                </html>";

            await _emailService.SendEmail(convite.Aluno.Email, assunto, mensagem);
        }

        public async Task<bool> VerificaVinculo(ConviteRequestDTO conviteDto) {
            var professor = await _daoProfessor.SelectProfessorByIdUsuario(conviteDto.IdProfessorUsuario);
            var vinculoExiste = await _daoConvite.SelectVinculoAlunoProfessor(conviteDto.IdAluno, professor.Id);
            return vinculoExiste != "VINCULADO" ? false : true;
        }

        private bool verificarEmailAluno(Aluno aluno, string emailDestinatario) {
            return aluno.Email == emailDestinatario;
        }
    }
}
