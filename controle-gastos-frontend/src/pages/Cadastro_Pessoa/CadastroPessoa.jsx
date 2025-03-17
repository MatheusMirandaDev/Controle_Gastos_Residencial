import { useEffect, useState } from "react";
import "./CadastroPessoa.css";
import Lixo from "../../assets/lixo.svg";
import Lapis from "../../assets/Lapis.svg";
import api from "../../services/api";

function Cadastro_Pessoa() {
  const [pessoas, setPessoas] = useState([]);
  const [idade, setIdade] = useState("");
  const [nome, setNome] = useState("");
  const [erroNome, setErroNome] = useState(false);
  const [erroIdade, setErroIdade] = useState(false);
  const [modoEdicao, setModoEdicao] = useState(false);
  const [idEditando, setIdEditando] = useState(null);

  async function getPessoas() {
    try {
      const response = await api.get("/Pessoa");
      setPessoas(response.data);
    } catch (error) {
      console.error("Erro ao obter pessoas:", error);
    }
  }

  async function createPessoa() {
    if (!validarCampos()) return;

    try {
      await api.post("/Pessoa", { nome: nome.trim(), idade: Number(idade) });
      resetForm();
      getPessoas();
    } catch (error) {
      console.error("Erro ao criar pessoa:", error);
    }
  }

  async function putPessoa() {
    if (!validarCampos()) return;

    try {
      await api.put(`/Pessoa/${idEditando}`, { nome: nome.trim(), idade: Number(idade) });
      resetForm();
      getPessoas();
    } catch (error) {
      console.error("Erro ao atualizar pessoa:", error);
      alert("Erro ao atualizar pessoa.");
    }
  }

  async function deletePessoa(id) {
    try {
      await api.delete(`/Pessoa/${id}`);
      getPessoas();
    } catch (error) {
      console.error("Erro ao deletar pessoa:", error);
    }
  }

  function validarCampos() {
    const nomeValido = nome.trim() !== "";
    const idadeValida = /^\d+$/.test(idade) && Number(idade) >= 1 && Number(idade) <= 122;
    setErroNome(!nomeValido);
    setErroIdade(!idadeValida);

    if (!nomeValido || !idadeValida) {
      alert("Nome e idade são obrigatórios e devem ser válidos.");
      return false;
    }
    return true;
  }

  function editarPessoa(pessoa) {
    setModoEdicao(true);
    setIdEditando(pessoa.id);
    setNome(pessoa.nome);
    setIdade(String(pessoa.idade));
  }

  function resetForm() {
    setNome("");
    setIdade("");
    setModoEdicao(false);
    setIdEditando(null);
  }

  useEffect(() => {
    getPessoas();
  }, []);

  return (
    <div className="container">
      <form>
        <h1>{modoEdicao ? "Editar Pessoa" : "Cadastro de Pessoa"}</h1>
        <input className={erroNome ? "erro" : ""} placeholder="Nome*" type="text" value={nome} onChange={(e) => setNome(e.target.value)} required />
        <input className={erroIdade ? "erro" : ""} placeholder="Idade*" type="text" value={idade} onChange={(e) => setIdade(e.target.value)} required />
        {modoEdicao ? (
          <>
            <button type="button" onClick={putPessoa}>Salvar</button>
            <button type="button" onClick={resetForm}>Cancelar</button>
          </>
        ) : (
          <button type="button" onClick={createPessoa}>Cadastrar</button>
        )}
      </form>
      <h2>Pessoas Cadastradas</h2>
      {pessoas.map((pessoa) => (
        <div key={pessoa.id} className="card">
          <div>
            <p>Nome: <span>{pessoa.nome}</span></p>
            <p>Idade: <span>{pessoa.idade}</span></p>
          </div>
          <button onClick={() => editarPessoa(pessoa)}><img src={Lapis} alt="Editar" /></button>
          <button onClick={() => deletePessoa(pessoa.id)}><img src={Lixo} alt="Excluir" /></button>
        </div>
      ))}
    </div>
  );
}

export default Cadastro_Pessoa;
