using Microsoft.EntityFrameworkCore;
using WebApiFuncionario.DataContext;
using WebApiFuncionario.Models;

namespace WebApiFuncionario.Service.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly AplicationDbContext _context;
        public FuncionarioService(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel newFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                if(newFuncionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Message = "Insira os dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                newFuncionario.DataCriacao = DateTime.Now.ToLocalTime();
                newFuncionario.DataAlteracao = DateTime.Now.ToLocalTime();

                _context.Add(newFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucesso=false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Message = "Usuário não encontrado";
                    serviceResponse.Sucesso = false;
                }

                funcionario.DataAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();

            }
            catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucesso = false;  
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionario()
        {
            //Criando uma Lista de FuncionarioModel dentro do ServiceResponse.
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                serviceResponse.Dados = _context.Funcionarios.ToList();

                if(serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Message = "Nenhum dado encontrado!!";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucesso = false;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel> { };

            try
            {
                FuncionarioModel funcionario = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Message = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                }
                serviceResponse.Dados = funcionario;
            }
            catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Message = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }

                funcionario.Ativo = false;
                funcionario.DataAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();

            }
            catch( Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editedFuncionario)
        {
           ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == editedFuncionario.Id);
                
                if(funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Message = "Usuário não encontrado";
                    serviceResponse.Sucesso = false;
                }

                funcionario.DataAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(editedFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados= _context.Funcionarios.ToList();

            }
            catch( Exception ex )
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
