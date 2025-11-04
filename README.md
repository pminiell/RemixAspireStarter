This is a full-stack web application built to demonstrate modern development practices. The solution consists of a **Minimal API** backend with **CQRS patterns** (MediatR & AutoMapper are included, but overkill for simple CRUD operations). A **React** client application built with **Vite**, **React Router**, and **Tailwind CSS**. 

---

Thanks to Julio Casal for this blog which helped immmensely with the Vite configuration - [https://juliocasal.com/blog/going-full-stack-with-dotnet-aspire](Going Full-Stack With .NET & Aspire).

---

The AppHost starts the application. 

It adds the Postgres resource before kicking off the Worker service which registers host application services and seeds the database and runs migrations. 

The API project contains the minimal API controllers and CQRS handlers.

Finally the Client project is started and assigns a port to serve the Vite application. A proxy ports request from the client to the API injecting whatever port Aspire assigns to the Node project. 

