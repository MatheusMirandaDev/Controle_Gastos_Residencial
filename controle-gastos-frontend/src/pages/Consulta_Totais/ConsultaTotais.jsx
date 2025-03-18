import { useEffect, useState } from "react";
import "./ConsultaTotais.css";
import api from "../../services/api";

function ConsultaTotais() {
  const [totaisGerais, setTotaisGerais] = useState({});
  const [totaisPorPessoa, setTotaisPorPessoa] = useState([]);

  async function getTotais() {
    try {
      const response = await api.get("/ConsultaTotais");
      setTotaisPorPessoa(response.data || []); // Garante que sempre haverá um array
    } catch (error) {
      console.error("Erro ao buscar os totais por pessoa:", error);
    }
  }

  async function getTotaisGerais() {
    try {
      const response = await api.get("/ConsultaTotais/gerais");
      setTotaisGerais(response.data || {}); // Garante que sempre haverá um objeto
    } catch (error) {
      console.error("Erro ao buscar os totais gerais:", error);
    }
  }

  useEffect(() => {
    getTotais();
    getTotaisGerais();
  }, []);

  const getColorClass = (value) => {
    if (value === 0) {
      return "zero";
    }
    return value < 0 ? "negativo" : "positivo"; // Saldo negativo ou positivo
  };

  return (
    <div className="container">
      <h1>Totais por Pessoa</h1>
      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Receitas (R$)</th>
            <th>Despesas (R$)</th>
            <th>Saldo (R$)</th>
          </tr>
        </thead>
        <tbody>
          {totaisPorPessoa.length > 0 ? (
            totaisPorPessoa.map((item) => (
              <tr key={item.nome}>
                <td>{item.nome}</td>
                <td className="positivo">
                  R$ {item.totalReceitas?.toFixed(2) || "0.00"}
                </td>
                <td className="negativo">
                  R$ {item.totalDespesas?.toFixed(2) || "0.00"}
                </td>
                <td className={getColorClass(item.saldo)}>
                  R$ {item.saldo?.toFixed(2) || "0.00"}
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="4" style={{ textAlign: "center" }}>
                Nenhum dado disponível
              </td>
            </tr>
          )}
        </tbody>
      </table>

      <div className="totais-gerais">
        <h1>Totais Gerais</h1>
        <p className="positivo">
          Total de Receitas: R${" "}
          {totaisGerais.totalReceitas?.toFixed(2) || "0.00"}
        </p>
        <p className="negativo">
          Total de Despesas: R${" "}
          {totaisGerais.totalDespesas?.toFixed(2) || "0.00"}
        </p>
        <p className={getColorClass(totaisGerais.saldo)}>
          Saldo Líquido: R$ {totaisGerais.saldo?.toFixed(2) || "0.00"}
        </p>
      </div>
    </div>
  );
}

export default ConsultaTotais;
