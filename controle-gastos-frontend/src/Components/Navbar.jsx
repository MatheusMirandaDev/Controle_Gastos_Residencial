import "./NavBar.css"; // Importa o arquivo de estilos para o componente NavBar
import { Link } from "react-router-dom"; // Importa o componente Link do react-router-dom para navegação

const NavBar = () => {
  return (
    <div className="navbar">
      {" "}
      {/* Contêiner principal do componente NavBar */}
      <nav>
        {/* Lista de navegação com links para as diferentes páginas */}
        <ul>
          <li>
            {/* Link para a página inicial */}
            <Link to="/">Home</Link>
          </li>
          <li>
            {/* Link para a página de cadastro de pessoas */}
            <Link to="/CadastroDePessoas">Cadastro de Pessoas</Link>
          </li>
          <li>
            {/* Link para a página de cadastro de transações */}
            <Link to="/CadastroDeTrasacoes">Cadastro de Transações</Link>
          </li>
          <li>
            {/* Link para a página de consulta de totais */}
            <Link to="/ConsultaDeTotais">Consulta de Totais</Link>
          </li>
        </ul>
      </nav>
    </div>
  );
};

export default NavBar;
