# Death Counter (Contador de Mortes)

Um contador de mortes manual para ser utilizado como widget na sua stream! (ele não lê a memória do jogo, precisa ser ativado manualmente, sem risco de banimentos)
Criado com o intuito para ser utilizado em jogos da franquia SoulsBorne, mas pode ser utilizado em qualquer tipo de jogo.

# Funções
* Botão direito do mouse - Funções principais
  * Definir Jogo
    * Título do jogo que será jogado
  * Definir Texto de Morte
    * Texto de morte customizável
  * Salvar Stats
    * Salva na pasta do programa um arquivo chamado GameStats.ini com as informações salvas do player!
    * OBS: quando fechar o programa e abrir novamente, ele irá ler as informações desse arquivo, possibilitando assim continuar de onde parou
  * Salvar Configurações
    * Salva na pasta do programa um arquivo chamado config.ini com as informações de textos configuradas pelo player!
  * Sair
    * Fecha completamente o programa

# Como utilizar
* As teclas (por enquanto fixas) são NUMPAD1 para aumentar e NUMPAD2 para diminuir
* Ao finalizar ou a qualquer momento, salve tudo para não perder!

# Linguagem: C# WPF c/ .NET Core 8
 
## Bugs/TODO
- [ ] Atalhos não funcionando quando estão sem foco na janela
- [ ] Personalização de teclas

## Desenvolvimento

* Configuração inicial do ambiente
```
mkdir DeathCounter
cd DeathCounter
git clone https://github.com/Ro0ds/DeathCounter.git
dotnet watch run
```
