import { useEffect, useState } from "react"; // Importa os hooks necessários
import "./CadastroTransacao.css"; // Importa os estilos específicos para o cadastro de transações
import api from "../../services/api"; // Importa o serviço de API para interação com o backend

function CadastroTransacao() {
  // Estados para armazenar informações das transações, descrição, valor, tipo e pessoa
  const [transacoes, setTransacao] = useState([]); // Armazena todas as transações
  const [descricao, setDescricao] = useState(""); // Armazena a descrição da transação
  const [valor, setValor] = useState(""); // Armazena o valor da transação
  const [tipo, setTipo] = useState("Despesa"); // define o tipo da transação (Despesa ou Receita)
  const [pessoaId, setPessoaId] = useState(""); // Armazena o id da pessoa associada à transação
  const [pessoas, setPessoas] = useState([]); // Armazena todas as pessoas cadastradas
  const [pessoaSelecionada, setPessoaSelecionada] = useState(null); // Armazena a pessoa selecionada para a transação

  // Estados para controle de erro nos campos
  const [erroDescricao, setErroDescricao] = useState(false); // Erro na descrição
  const [erroValor, setErroValor] = useState(false); // Erro no valor
  const [erroPessoaId, setErroPessoaId] = useState(false); // Erro na pessoa selecionada

  // Função assíncrona para buscar todas as transações da API
  async function getTransacao() {
    const transacaoFromApi = await api.get("/Transacao"); // Buscar as transações
    setTransacao(transacaoFromApi.data); // Atualiza o estado com as transações retornadas
  }

  // Função assíncrona para buscar todas as pessoas da API
  async function getPessoas() {
    const pessoasFromApi = await api.get("/Pessoa"); // Buscar as pessoas cadastradas
    setPessoas(pessoasFromApi.data); // Atualiza o estado com as pessoas retornadas
  }

  // Função para criar uma nova transação
  async function createTransacao() {
    let erro = false; // Inicializa a variável erro

    // Validação dos campos obrigatórios para ver se todos estão preenchidos
    if (!descricao) {
      setErroDescricao(true);
      erro = true;
    } else {
      setErroDescricao(false);
    }

    if (!valor) {
      setErroValor(true);
      erro = true;
    } else {
      setErroValor(false);
    }

    if (!pessoaId) {
      setErroPessoaId(true);
      erro = true;
    } else {
      setErroPessoaId(false);
    }

    if (erro) {
      alert("O(s) campo(s) em vermelho é(sao) obrigatorio(s)!"); // Exibe o alerta de erro
      return;
    }

    try {
      // Lógica para definir o tipo de transação baseado na idade da pessoa
      const tipoConvertido =
        pessoaSelecionada && pessoaSelecionada.idade < 18
          ? 1 // Menores de idade só podem registrar "Despesa" (1)
          : tipo === "Despesa"
          ? 1 // "Despesa" é representada por 1
          : 0; // "Receita" é representada por 0

      const newTransacao = {
        descricao,
        valor: parseFloat(valor).toFixed(2), // Formata o valor para duas casas decimais
        tipo: tipoConvertido,
        pessoaId: parseInt(pessoaId), // Converte o id da pessoa para inteiro
      };

      await api.post("/Transacao", newTransacao); // Envia a transação para a API

      // Limpa os campos após o cadastro
      setDescricao("");
      setValor("");
      setTipo("Despesa");
      setPessoaId("");
      getTransacao(); // Atualiza a lista de transações
    } catch (error) {
      console.error("Erro ao criar transação:", error.response?.data || error); // Exibe erro no console
      alert("Erro ao criar transação!"); // Exibe um alerta de erro
    }
  }

  // Função para selecionar a pessoa associada à transação
  function handlePessoaSelect(event) {
    const selectedPessoaId = event.target.value; // Pega o id da pessoa selecionada
    setPessoaId(selectedPessoaId); // Atualiza o id da pessoa

    // Busca a pessoa selecionada pela id
    const pessoa = pessoas.find((p) => p.id === parseInt(selectedPessoaId));
    setPessoaSelecionada(pessoa); // Atualiza a pessoa selecionada
  }

  // Efeito para buscar as transações e pessoas ao carregar o componente
  useEffect(() => {
    getTransacao(); //
    getPessoas();
  }, []);

  return (
    <div className="container">
      <h1>Cadastro de Transação</h1>
      <form
        onSubmit={(e) => {
          e.preventDefault(); // Impede o comportamento padrão de recarregar a página ao submeter o formulário
          createTransacao(); // Chama a função para criar uma nova transação
        }}
      >
        <div>
          <label>Pessoa referente à transferência</label>
          <select
            value={pessoaId}
            onChange={handlePessoaSelect} // Atualiza o estado de pessoaId ao selecionar uma pessoa
            className={erroPessoaId ? "input-erro" : ""} // Aplica estilo de erro se houver erro
          >
            <option value="">Selecione uma pessoa*</option>
            {pessoas.map((pessoa) => (
              <option key={pessoa.id} value={pessoa.id}>
                {pessoa.nome} ({pessoa.idade} anos)
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Valor*</label>
          <input
            type="number"
            value={valor}
            onChange={(e) => setValor(e.target.value)} // Atualiza o estado de valor ao digitar
            className={erroValor ? "input-erro" : ""} // Aplica estilo de erro se houver
          />
        </div>
        <div>
          <label>Descrição*</label>
          <input
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)} // Atualiza o estado de descricao ao digitar
            className={erroDescricao ? "input-erro" : ""} // Aplica estilo de erro se houver erro
          />
        </div>
        <div>
          <label>Tipo*</label>
          <select
            value={tipo}
            onChange={(e) => setTipo(e.target.value)} // Atualiza o estado de tipo ao selecionar
            disabled={pessoaSelecionada && pessoaSelecionada.idade < 18} // Desabilita a seleção de "Receita" para menores de 18
          >
            <option value="Despesa">Despesa</option>
            {!(pessoaSelecionada && pessoaSelecionada.idade < 18) && (
              <option value="Receita">Receita</option>
            )}
          </select>
        </div>
        <button type="submit">Cadastrar</button>
      </form>

      <h2>Transações Cadastradas</h2>
      <div className="tabela-container">
        {/* Tabela para exibir as transações cadastradas */}
        <table className="tabela-planilha">
          <thead>
            <tr>
              {/* Exibe os dados (nome, valor, descrição, tipo) da pessoa da transação */}
              <th>Pessoa</th> 
              <th>Valor</th>
              <th>Descrição</th>
              <th>Tipo</th>
            </tr>
          </thead>
          <tbody>
            {/* Mapeia e renderiza todas as transações cadastradas pelo map*/}
            {transacoes.map((transacao, index) => (
              <tr key={transacao.id || `transacao-${index}`}>
                <td>{transacao.pessoa?.nome}</td>
                {/* Exibe as informações da transação */}
                <td>R$ {transacao.valor}</td>
                <td className="descricao">{transacao.descricao}</td>
                <td
                  className={transacao.tipo === 1 ? "despesa" : "receita"} // Define a classe CSS com base no tipo da transação
                >
                  <strong>
                    {transacao.tipo === 1 ? "Despesa" : "Receita"}
                  </strong>
                  {/* Exibe o tipo da transação */}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default CadastroTransacao;
