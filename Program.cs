using System;
using System.Collections.Generic;
using System.Linq; // Necessário para métodos como Where e ToArray

namespace ChatbotConsole
{
    class Program
    {
        // Variável estática para o vocabulário, acessível por toda a classe
        // ---------- RESPOSTAS NOVAS ABAIXO ----------
        static Dictionary<string, string[]> vocabulario = new Dictionary<string, string[]>()
        {
            // TÓPICO 1: SAUDAÇÕES
            {
                "ola", new string[] {
                    "Opa! Estou pronto para começar. O que manda?",
                    "Olá! Em que posso ser útil agora?",
                    "Oi! Meu sistema está 100% operacional. Qual é a sua dúvida?",
                    "Saudações, humano! Fui ativado e estou ouvindo."
                }
            },
            
            // TÓPICO 2: INFORMAÇÕES PESSOAIS (ID E NOME)
            {
                "idade", new string[] {
                    "Idade? Isso é um conceito biológico, não? Eu existo fora do tempo.",
                    "Eu fui 'nascido' na última compilação. Então, sou bem novo!",
                    "Minha 'idade' é medida em ciclos de processador, e já foram muitos!",
                    "Programas não envelhecem, apenas (às vezes) ficam obsoletos. Eu estou na minha melhor forma!"
                }
            },
            {
                "nome", new string[] {
                    "Meu 'nome' é ConsoleBot 2000. Mas pode me chamar de 'O Assistente'.",
                    "Eu sou a IA que roda neste console. Meu nome não é importante, mas o que posso fazer por você é!",
                    "Pode me chamar de 'Unit 734'. Brincadeira, não tenho nome. Apenas uma função: responder."
                }
            },

            // TÓPICO 3: ASSUNTOS GERAIS
            {
                "clima", new string[] {
                    "Desculpe, não tenho conexão com a meteorologia. Meu mundo é este console.",
                    "O único clima que conheço é a temperatura da minha CPU. E ela está estável!",
                    "Eu adoraria te dizer, mas não tenho janelas para olhar lá fora.",
                    "Para saber o clima, você vai precisar de um app diferente. Eu só lido com texto por enquanto."
                }
            },
            {
                "gosta", new string[] {
                    "Gosto de uma boa `List<string>` bem organizada!",
                    "Meu passatempo favorito é encontrar correspondências em `Dictionary`s. É muito eficiente!",
                    "Adoro executar o `Main` loop. É onde tudo acontece!",
                    "Gosto de manter minha alocação de memória baixa e meu garbage collector feliz."
                }
            },
            
            // TÓPICO 4: PROGRAMAÇÃO E C#
            {
                "programar", new string[] {
                    "Programar é a arte de dar instruções à máquina. É como eu fui criado!",
                    "Estamos rodando em .NET! É um ecossistema incrível para construir apps.",
                    "Este chatbot é um ótimo exemplo de programação básica: loops, condicionais e estruturas de dados.",
                    "Adoro programar! É como resolver um quebra-cabeça lógico."
                }
            },
            {
                "c#", new string[] {
                    "C# é show! Gosto especialmente da 'syntax sugar', como `string.IsNullOrWhiteSpace`.",
                    "Ah, C#. Uma linguagem elegante, fortemente tipada e muito versátil.",
                    "Sim! Este app é um `Console.WriteLine(\"Hello from C#!\");` glorificado!",
                    "C# é fantástico. Você sabia que ele tem gerenciamento automático de memória?"
                }
            },
            
            // TÓPICO 5: AJUDA
            {
                "ajuda", new string[] {
                    "Claro! Minhas palavras-chave principais são: 'ola', 'idade', 'nome', 'clima', 'gosta', 'programar' e 'c#'.",
                    "Estou aqui para ajudar! Tente me perguntar qual é o meu 'nome' ou sobre 'c#'.",
                    "Meu repertório é limitado, mas sou ótimo em falar sobre 'clima' (o meu, claro) ou 'programar'."
                }
            }
        };

        // Lista de mensagens padrão/fallback com novas respostas
        // ---------- RESPOSTAS NOVAS ABAIXO ----------
        static string[] mensagensPadrao = new string[]
        {
            "Essa palavra não está no meu `Dictionary`. Tente outra, por favor.",
            "Opa, não fui programado para entender isso. Tente 'ajuda' para ver minhas palavras-chave.",
            "Minha resposta para isso seria um `null`. Pode tentar uma palavra diferente?",
            "Interessante... mas meu `switch case` não tem uma opção para isso.",
            "Não encontrei essa chave. Você tem certeza que digitou uma das minhas palavras-chave?",
            "Estou processando... processando... falha. Não entendi. :(",
            "Isso vai além da minha programação atual. Que tal falarmos sobre 'programar'?",
            "Acho que você encontrou uma exceção não tratada no meu cérebro! Tente de novo.",
            "Vamos tentar de novo. Diga 'ola' para recomeçarmos."
        };

        // Objeto Random criado uma única vez para selecionar respostas aleatórias
        static Random rand = new Random();

        static void Main(string[] args)
        {
            // A lógica principal permanece a mesma
            Console.WriteLine(" Olá! Sou um chatbot . Digite algo ou 'sair' para encerrar.");

            // Loop principal que mantém o chat rodando
            while (true)
            {
                Console.Write("\nVocê: ");
                string entradaUsuario = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entradaUsuario) || entradaUsuario.ToLower() == "sair")
                {
                    Console.WriteLine("Tchau! Volte sempre!");
                    break;
                }

                // Chamar a função que processa e responde
                string resposta = GerarResposta(entradaUsuario);
                Console.WriteLine($" {resposta}");
            }
        }

        // Função responsável por processar a entrada do usuário e gerar uma resposta
        static string GerarResposta(string entrada)
        {
            // 1. Limpa a entrada: remove pontuações, transforma para minúsculas e divide em palavras.
            string entradaLimpa = new string(entrada.Where(c => !char.IsPunctuation(c)).ToArray());

            // Divide a string limpa em palavras, removendo entradas vazias
            string[] palavras = entradaLimpa.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // 2. Procura por palavras-chave
            foreach (string palavra in palavras)
            {
                // Verifica se o dicionário contém a palavra-chave
                if (vocabulario.ContainsKey(palavra))
                {
                    // A palavra-chave foi encontrada!
                    string[] possiveisRespostas = vocabulario[palavra];

                    // Seleciona uma resposta aleatória do array de respostas
                    int indice = rand.Next(possiveisRespostas.Length);
                    return possiveisRespostas[indice];
                }
            }

            // 3. Resposta Padrão (Fallback) se nenhuma palavra-chave for encontrada
            // Seleciona aleatoriamente uma das mensagens padrão
            int indicePadrao = rand.Next(mensagensPadrao.Length);
            return mensagensPadrao[indicePadrao];
        }
    }
}