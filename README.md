### Para Rodar
1. Inicie o Docker
1. Execute no terminal```docker-compose up```
2. Acesse: **http://localhost:5000/swagger**


### Fase 2 - Perguntas ao PO
- O projeto poder� ser dividido em fases?
- O projeto poder� ser compartilhado com outros usu�rios?
- O projeto tem necessidade de ter uma visualiza��o de canlend�rio ou grantt?
- As tarefas poder� ter precedentes ou depend�ncias?
- Interessa guardar as datas de mudan�a de status da tarefa para gera��o de relat�rios?
- Futuramente os status da tarefa poder�o ser din�micos?
- � de interesse criar projetos modelos (template) para serem replicados a partir do modelo?
- O coment�rio poder� agregar rea��es de aprova��o e reprova��o?
- O coment�rio poder� ser exclu�do?
- Quanndo haver uma mudan�a de status de uma tarefa, � interessante notificar outros usu�rios?
- Caso for interessante o ponto anterior, qual melhor meio de notifica��o? Email, SMS, Telegram, Whatsapp, Slack e etc?
- � interessante colocar inteligencia artifical para que a partir de um promtpt gere a lista de tarefas?
- Poder� ter lembrete de conclus�o da tarefa?
- Poderemos colocar I.A. para identificar tarefas recorrentes de distintos projetos para criar uma solu��o j� pronta?

### Fase 3 - Melhorias
- Escreveria mais testes unit�rios e de integra��o (que acabei n�o criando)
- Criar uma esteira de CI/CD
- Colocar o SonarQube para garantir a entrega de qualidade
- Usar uma AWS ou Azure
- Colocar monitoriamento com Prometheus e Grafana
- Melhoria o levantamento de requisitos com especifica��es mais claras
- Manteria o Clean Code, SOLID e Clean Architecture
- Usaria o MS Devops para gerenciar as demandas
- Usaria Migrations no banco de dados para ter mais controle das altera��es
