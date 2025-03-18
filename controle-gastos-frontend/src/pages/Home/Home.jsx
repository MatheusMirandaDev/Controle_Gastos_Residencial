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
        no controle financeiro doméstico de maneira simples e eficiente. Ele
        permite o gerenciamento de receitas e despesas de cada pessoa da casa,
        auxiliando no controle do saldo geral.
      </p>
      <section>
        {/* Seção que descreve as funcionalidades do sistema */}
        <h2>Funcionalidades</h2>
        {/* Caixa de funcionalidades: Cadastro de Pessoas */}
        <div className="feature-box">
          <h3>Cadastro de Pessoas</h3> {/* Título da funcionalidade */}
          <p>
            Crie, visualize, atualize e remova registros de pessoas no sistema.
          </p>
          {/* Explicação do que a funcionalidade faz */}
          <h6>
            {/* Explicação sobre o comportamento ao excluir uma pessoa */}
            *Atenção: Ao excluir uma pessoa, todas as suas transações serão
            excluídas automaticamente.
          </h6>
        </div>
        {/* Caixa de funcionalidades: Cadastro de Transações */}
        <div className="feature-box">
          <h3>Cadastro de Transações</h3>
          <p>
            Crie, visualize, atualize e remova registros de transações no
            sistema.
          </p>
          <h6>
            {/* Restrição para menores de 18 anos não poderem registrar receitas */}
            *Atenção: Pessoas menores de 18 anos não podem registrar transações
            do tipo
            {' "receita"'}.
          </h6>
        </div>
        {/* Caixa de funcionalidades: Consulta de Totais */}
        <div className="feature-box">
          <h3>Consulta de Totais</h3>
          <p>
            Visualize o total de receitas, despesas e o saldo individual, além
            do saldo geral da residência.
          </p>
        </div>
        <h2>Como Usar o Sistema?</h2> {/* Subtítulo com instruções de uso */}
        {/* Passo a passo de como usar as funcionalidades do sistema */}
        {/* Passo 1: Cadastro de Pessoas */}
        <div className="feature-box">
          <h3>1º - Cadastro de Pessoas</h3>
          <p>
            - Acesse a página <strong>{' "Cadastro de Pessoas"'}</strong> pela
            barra de navegação.{" "}
          </p>
          <p>
            - Visualize todas as pessoas cadastradas e adicione ou remova
            registros conforme necessário.
          </p>
        </div>
        {/* Passo 2: Cadastro de Transações */}
        <div className="feature-box">
          <h3>2º - Cadastro de Transações</h3>
          <p>
            - Na página de <strong>{' "Cadastro de Transações"'}</strong>,
            registre despesas ou receitas para cada pessoa.
          </p>
          <p>
            - No final da tela, você poderá visualizar as transações registradas
            e seus responsáveis.
          </p>
        </div>
        {/* Passo 3: Consulta de Totais */}
        <div className="feature-box">
          <h3>3º - Consulta de Totais</h3>
          <p>
            - Acesse a página de <strong>{' "Consulta de Totais"'}</strong> para
            visualizar o saldo individual e o total geral da residência.
          </p>
        </div>
      </section>
    </div>
  );
};

export default Home;
