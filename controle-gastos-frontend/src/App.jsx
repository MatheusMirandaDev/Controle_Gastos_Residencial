import { Outlet } from "react-router-dom"; // Importa Outlet para renderizar componentes de rota filhos
import NavBar from "./Components/NavBar"; // Importa o componente NavBar

// responsavel por renderizar o componente NavBar e o componente correspondente à rota atual
const App = () => {
  return (
    <div className="App">
      <NavBar />
      <Outlet />
    </div>
  );
};

export default App; // Exporta o componente App como padrão
