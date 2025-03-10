import "./Home.css"; // Importa o arquivo de estilos CSS para aplicar o design

// Componente principal que representa a página Home do sistema
const Home = () => {
  return (
    <div className="container">
      {" "}
      {/* Contêiner geral da página para centralizar o conteúdo */}
      {/* Título principal explicando o objetivo do sistema */}
      <h1>Sistema de Gestão de Gastos Residenciais</h1>
      {/* Descrição geral do funcionamento do sistema */}
      <p>
        O Sistema de Gestão de Gastos Residenciais foi desenvolvido para ajudar
        no controle das finanças domésticas de maneira simples e eficiente. Ele
        permite o gerenciamento de despesas e receitas de cada pessoa da casa,
        auxiliando no controle do saldo geral.
      </p>
      <section>
        {" "}
        {/* Seção que descreve as funcionalidades do sistema */}
        <h2>Funcionalidades</h2>
        {/* Caixa de funcionalidades: Cadastro de Pessoas */}
        <div className="feature-box">
          <h3>Cadastro de Pessoas</h3> {/* Título da funcionalidade */}
          <p>Crie, visualize e remova registros de pessoas no sistema.</p>{" "}
          {/* Explicação do que a funcionalidade faz */}
          <h6>
            {" "}
            {/* Explicação sobre o comportamento ao excluir uma pessoa */}* Ao
            excluir uma pessoa, todas as suas transações serão excluídas
            automaticamente.
          </h6>
        </div>
        {/* Caixa de funcionalidades: Cadastro de Transações */}
        <div className="feature-box">
          <h3>Cadastro de Transações</h3>
          <p>
            Registre receitas e despesas, associando cada transação a uma pessoa
            da casa.
          </p>
          <h6>
            {" "}
            {/* Restrição para menores de 18 anos não poderem registrar receitas */}
            * Pessoas menores de 18 anos não podem registrar transações do tipo
            ´´receita``.
          </h6>
        </div>
        {/* Caixa de funcionalidades: Consulta de Totais */}
        <div className="feature-box">
          <h3>Consulta de Totais</h3>
          <p>
            Visualize o total de receitas, despesas e o saldo individual de cada
            pessoa, além do saldo geral da residência.
          </p>
        </div>
        <h2>Como Usar o Sistema?</h2> {/* Subtítulo com instruções de uso */}
        {/* Passo a passo de como usar as funcionalidades do sistema */}
        {/* Passo 1: Cadastro de Pessoas */}
        <div className="feature-box">
          <h3>1º - Cadastro de Pessoas</h3>
          <p>
            Adicione uma pessoa ao sistema. No topo da tela, há uma barra de
            navegação. Clique em <em>´´Cadastro de Pessoas``</em>. Nessa página,
            você poderá visualizar todas as pessoas cadastradas no sistema e
            terá a opção de adicionar ou excluir registros.
          </p>
        </div>
        {/* Passo 2: Cadastro de Transações */}
        <div className="feature-box">
          <h3>2º - Cadastro de Transações</h3>
          <p>
            Cadastre as transações (despesas ou receitas) para cada pessoa,
            conforme o tipo de gasto. Dê uma descrição para a transação. No topo
            da tela, há uma barra de navegação.
            <em>´´Cadastro de Transações``</em>. Na aba de transações, você
            poderá visualizar todas as transações registradas e seus respectivos
            responsáveis ao final da tela, embaixo do formulario.
          </p>
        </div>
        {/* Passo 3: Consulta de Totais */}
        <div className="feature-box">
          <h3>3º - Consulta de Totais</h3>
          <p>
            Consulte os totais de receitas, despesas e saldo de cada pessoa,
            além do total geral da residência. Para acessar essa funcionalidade,
            vá até a barra de navegação no topo da tela e clique em
            <em>´´Consulta de Totais``</em>.
          </p>
        </div>
      </section>
    </div>
  );
};

export default Home; // Exporta o componente Home para ser utilizado em outras partes do projeto
