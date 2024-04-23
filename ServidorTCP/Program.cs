using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{
    public class ServidorTCP
    {
        public static void Main()
        {
            // Defina a porta em que o servidor estará ouvindo conexões
            int porta = 8080;

            // Crie uma instância TcpListener para ouvir conexões na porta especificada
            TcpListener servidor = new TcpListener(IPAddress.Any, porta);

            try
            {
                // Comece a ouvir por conexões
                servidor.Start();
                Console.WriteLine("Servidor iniciado. Aguardando conexões...");

                while (true)
                {
                    // Aceita uma conexão pendente
                    TcpClient cliente = servidor.AcceptTcpClient();
                    Console.WriteLine("Cliente conectado.");

                    // Obtenha a stream de entrada e saída para a conexão
                    NetworkStream stream = cliente.GetStream();

                    // Buffer para armazenar os dados recebidos
                    byte[] buffer = new byte[1024];
                    StringBuilder mensagem = new StringBuilder();

                    int bytesLidos;
                    // Leia os dados do cliente
                    while ((bytesLidos = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        // Decodifique os dados em uma mensagem de texto
                        string dados = Encoding.ASCII.GetString(buffer, 0, bytesLidos);
                        mensagem.Append(dados);
                    }

                    // Exiba a mensagem recebida
                    Console.WriteLine("Mensagem do cliente: " + mensagem);

                    // Responda ao cliente (apenas um exemplo)
                    byte[] resposta = Encoding.ASCII.GetBytes("Mensagem recebida com sucesso!");
                    stream.Write(resposta, 0, resposta.Length);

                    // Encerre a conexão com o cliente
                    cliente.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            finally
            {
                // Pare o servidor quando terminar
                servidor.Stop();
            }
        }
    }
}
