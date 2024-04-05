### Para Rodar
1. Inicie o Docker
1. Execute no terminal```docker-compose up```
2. Acesse: **http://localhost:5000/swagger**


### Fase 2 - Perguntas ao PO
- O projeto poderá ser dividido em fases?
- O projeto poderá ser compartilhado com outros usuários?
- O projeto tem necessidade de ter uma visualização de canlendário ou grantt?
- As tarefas poderá ter precedentes ou dependências?
- Interessa guardar as datas de mudança de status da tarefa para geração de relatórios?
- Futuramente os status da tarefa poderão ser dinâmicos?
- É de interesse criar projetos modelos (template) para serem replicados a partir do modelo?
- O comentário poderá agregar reações de aprovação e reprovação?
- O comentário poderá ser excluído?
- Quanndo haver uma mudança de status de uma tarefa, é interessante notificar outros usuários?
- Caso for interessante o ponto anterior, qual melhor meio de notificação? Email, SMS, Telegram, Whatsapp, Slack e etc?
- É interessante colocar inteligencia artifical para que a partir de um promtpt gere a lista de tarefas?
- Poderá ter lembrete de conclusão da tarefa?
- Poderemos colocar I.A. para identificar tarefas recorrentes de distintos projetos para criar uma solução já pronta?

### Fase 3 - Melhorias
- Escreveria mais testes unitários e de integração (que acabei não criando)
- Criar uma esteira de CI/CD
- Colocar o SonarQube para garantir a entrega de qualidade
- Usar uma AWS ou Azure
- Colocar monitoriamento com Prometheus e Grafana
- Melhoria o levantamento de requisitos com especificações mais claras
- Manteria o Clean Code, SOLID e Clean Architecture
- Usaria o MS Devops para gerenciar as demandas
- Usaria Migrations no banco de dados para ter mais controle das alterações
