import { useEffect, useState } from "react"; // Importa os hooks useState e useEffect do React para gerenciar estado e efeitos colaterais
import "./ConsultaTotais.css"; // Importa o arquivo CSS para estilização do componente
import api from "../../services/api"; // Importa a instância da API para fazer as requisições HTTP

function ConsultaTotais() {
  // Declaração de estados para armazenar os dados de totais gerais e totais por pessoa
  const [totaisGerais, setTotaisGerais] = useState({});
  const [totaisPorPessoa, setTotaisPorPessoa] = useState([]);

  // Função para buscar os totais de cada pessoa, como receitas, despesas e saldo
  async function getTotais() {
    try {
      // Faz a requisição GET para buscar os totais de receitas, despesas e saldo por pessoa
      const response = await api.get("/ConsultaTotais");
      setTotaisPorPessoa(response.data); // Atualiza o estado com os dados recebidos da API
    } catch (error) {
      // Caso ocorra algum erro durante a requisição, imprime no console
      console.error("Erro ao buscar os totais por pessoa:", error);
    }
  }

  // Função assíncrona para buscar os totais gerais (receitas, despesas e saldo)
  async function getTotaisGerais() {
    try {
      // Faz a requisição GET para buscar os totais gerais
      const response = await api.get("/ConsultaTotais/gerais");
      setTotaisGerais(response.data); // Atualiza o estado com os dados gerais recebidos da API
    } catch (error) {
      // Caso ocorra algum erro durante a requisição, imprime no console
      console.error("Erro ao buscar os totais gerais:", error);
    }
  }

  // useEffect é usado para carregar os dados assim que o componente for montado
  useEffect(() => {
    getTotais(); // Chama a função para buscar os totais por pessoa
    getTotaisGerais(); // Chama a função para buscar os totais gerais
  }, []); // O array vazio [] garante que a função seja executada apenas uma vez, no momento da montagem do componente

  // Função para determinar a classe de cor baseada no valor
  const getColorClass = (value) => {
    // Se o valor for negativo, retorna a classe "negative" para aplicar a cor vermelha
    return value < 0 ? "negative" : "";
  };

  return (
    <div className="container">
      <h2>Totais por Pessoa</h2>
      {/* Tabela para exibir os totais de cada pessoa */}
      <table>
        <thead>
          <tr>
            <th>Nome</th> {/* Cabeçalho da coluna para o nome da pessoa */}
            <th>Receitas (R$)</th> {/* Cabeçalho da coluna para as receitas */}
            <th>Despesas (R$)</th> {/* Cabeçalho da coluna para as despesas */}
            <th>Saldo (R$)</th> {/* Cabeçalho da coluna para o saldo */}
          </tr>
        </thead>
        <tbody>
          {/* Mapeia os totais por pessoa e exibe os dados de cada uma na tabela */}
          {totaisPorPessoa.map((item) => (
            <tr key={item.nome}>
              {/* A chave é o nome da pessoa para garantir a unicidade das linhas */}
              <td>{item.nome}</td> {/* Exibe o nome da pessoa */}
              <td className={getColorClass(item.totalReceitas)}>
                R$ {item.totalReceitas.toFixed(2)} {/* Exibe o total de receitas formatado com duas casas decimais */}
              </td>
              <td className="negative">
                R$ {item.totalDespesas.toFixed(2)} {/* Exibe o total de despesas, que sempre ficará vermelho */}
              </td>
              <td className={getColorClass(item.saldo)}>
                R$ {item.saldo.toFixed(2)} {/* Exibe o saldo formatado com duas casas decimais */}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {/* Seção para exibir os totais gerais de receitas, despesas e saldo */}
      <div className="totais-gerais">
        <h2>Totais Gerais</h2>
        <p>
          Total de Receitas: R$
          {totaisGerais.totalReceitas?.toFixed(2) || "0.00"} {/* Exibe o total de receitas */}
        </p>
        <p className="negative">
          Total de Despesas: R$
          {totaisGerais.totalDespesas?.toFixed(2) || "0.00"} {/* Exibe o total de despesas em vermelho */}
        </p>
        <p className={getColorClass(totaisGerais.saldo)}>
          Saldo Líquido: R$
          {totaisGerais.saldo?.toFixed(2) || "0.00"} {/* Exibe o saldo líquido geral, vermelho se negativo */}
        </p>
      </div>
    </div>
  );
}

export default ConsultaTotais; // Exporta o componente ConsultaTotais para que possa ser utilizado em outros componentes ou páginas
