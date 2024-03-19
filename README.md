# ChatAI


Проект ChatAI представляет собой инновационную платформу для создания чат-ботов, которая позволяет интегрировать несколько ботов на основе алгоритмов поиска (ретривер-ботов) с генеративными моделями, включая локально запущенные модели и модели, основанные на ChatGPT. Эта инициатива с открытым исходным кодом от "ООО ФракталТех" использует передовые возможности проектов, таких как [FractalGPT.SharpGPT](https://github.com/FractalGPT/SharpGPT), [SimpleLLMServer](https://github.com/FractalGPT/SimpleLLMServer) и [AIFramework версия 2.2 Open](https://github.com/AIFramework/AIFrameworkOpen), предлагая универсальное и масштабируемое решение для создания интеллектуальных агентов для ведения бесед.

## Основные особенности:

- **Интеграция генеративных ботов:** Включает интерфейс (`IGenerativeBot`) для поддержки различных генеративных чат-ботов, включая ChatGpt, GigaChat или любые локально развернутые большие языковые модели (LLMs), что позволяет вести динамичные и контекстуально актуальные беседы.

- **Ретривер-боты:** Использует коллекцию ретривер-ботов (`List<IRetryBot>`), которые являются алгоритмами, разработанными для извлечения информации, соответствующей запросу пользователя, повышая способность бота предоставлять точные и информативные ответы.

- **Осознание контекста:** Поддерживает контекст беседы (`BotContext`), чтобы обеспечить не только актуальность ответов, но и их персонализацию, создавая более увлекательный пользовательский опыт.

- **Поддержка разнообразных запросов:** Предлагает методы, такие как `GetSupport` и `GetRetriAnswer`, для взаимодействия как с ретривер, так и с генеративными ботами, получая поддерживающие тексты или прямые ответы на вопросы пользователей.

- **Асинхронные генеративные ответы:** Реализует событие (`GenerativeAnswer`) для асинхронной обработки ответов от генеративных ботов, что позволяет вести реальное взаимодействие и получать обратную связь.
- **Сбор датасета:** Собирает датасет в нужном формате, для дообучения внутренних LLM.
 
## Вклад в открытый исходный код:

Этот проект с открытым исходным кодом представлен компанией ООО "ФракталТех", приглашая разработчиков и энтузиастов ИИ вносить свой вклад, расширять и адаптировать платформу для разнообразных приложений. Благодаря совместной разработке мы стремимся постоянно улучшать возможности и охват этого инновационного решения для чат-ботов.

## Зависимости:

- **[FractalGPT.SharpGPT](https://github.com/FractalGPT/SharpGPT):** Библиотека для интеграции API генеративных моделей и предварительно обученных моделей(эмбеддеров), обеспечивающая основу для генеративных ответов чат-бота.

- **[AIFramework версия 2.2 Open](https://github.com/AIFramework/AIFrameworkOpen):** Открытый фреймворк ИИ, который предлагает различные инструменты и функциональные возможности, необходимые для разработки передовых приложений ИИ, включая обработку текста, сигналов и методы машинного обучения.
- **[SimpleLLMServer](https://github.com/FractalGPT/SimpleLLMServer):** Открытый проект на FastAPI, для запуска различных предварительно обученных генеративных нейросетей.

## Начало работы:

Разработчики, заинтересованные в участии в проекте или использовании **ChatAI**, могут клонировать репозиторий и изучить доступную документацию на страницах проекта на GitHub для [FractalGPT.SharpGPT](https://github.com/FractalGPT/SharpGPT), [SimpleLLMServer](https://github.com/FractalGPT/SimpleLLMServer) и [AIFramework версия 2.2 Open](https://github.com/AIFramework/AIFrameworkOpen). Проект разработан таким образом, чтобы быть гибким, позволяя легко интегрироваться с существующими системами или использоваться как самостоятельное решение для агентов ведения бесед.
