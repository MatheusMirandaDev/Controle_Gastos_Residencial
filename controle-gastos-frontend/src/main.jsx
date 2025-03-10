import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App";
import CadastroPessoa from "./pages/Cadastro_Pessoa/CadastroPessoa";
import CadastroTransacao from "./pages/Cadastro_Transacao/CadastroTransacao";
import ConsultaTotais from "./pages/Consulta_Totais/ConsultaTotais";
import Home from "./pages/Home/Home";
import "./index.css";

// Criação das rotas do navegador utilizando o react-router-dom
const router = createBrowserRouter([
  {
    path: "/", // A rota principal
    element: <App />, // Componente principal que será renderizado
    children: [
      { path: "", element: <Home /> }, // Rota padrão: carrega a página Home
      { path: "CadastroDePessoas", element: <CadastroPessoa /> }, // Rota para cadastro de pessoas
      { path: "CadastroDeTrasacoes", element: <CadastroTransacao /> }, // Rota para cadastro de transações
      { path: "ConsultaDeTotais", element: <ConsultaTotais /> }, // Rota para consultar totais
    ],
  },
]);

// Renderiza a aplicação React na raiz do documento HTML
createRoot(document.getElementById("root")).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
