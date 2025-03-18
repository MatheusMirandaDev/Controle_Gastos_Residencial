import { Link, useLocation } from "react-router-dom"; // Importa useLocation para saber a rota atual
import "./NavBar.css"; // Importa o arquivo de estilos para o componente NavBar

const NavBar = () => {
  const location = useLocation(); // Obtém a localização atual da página

  // Função para verificar se o link é o da rota atual
  const isActive = (path) => (location.pathname === path ? "active" : "");

  return (
    <div className="navbar">
      <nav>
        <ul>
          <li>
            <Link to="/" className={isActive("/")}>
              Home
            </Link>
          </li>
          <li>
            <Link
              to="/CadastroDePessoas"
              className={isActive("/CadastroDePessoas")}
            >
              Cadastro de Pessoas
            </Link>
          </li>
          <li>
            <Link
              to="/CadastroDeTransacoes"
              className={isActive("/CadastroDeTransacoes")}
            >
              Cadastro de Transações
            </Link>
          </li>
          <li>
            <Link
              to="/ConsultaDeTotais"
              className={isActive("/ConsultaDeTotais")}
            >
              Consulta de Totais
            </Link>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default NavBar;
