import {
  isRouteErrorResponse,
  Links,
  Meta,
  Outlet,
  Scripts,
  ScrollRestoration,
} from "react-router";
import type { Route } from "../.react-router/types/src/+types/root";
import "./app.css";

export const meta: Route.MetaFunction = () => {
  return [
    { title: "Remix Aspire Starter" },
    { name: "description", content: "A modern web application starter with React Router and .NET Aspire" },
  ];
};

export const links: Route.LinksFunction = () => {
  return [
    { rel: "icon", type: "image/svg+xml", href: "/vite.svg" },
  ];
};


// read more about this file https://reactrouter.com/api/framework-conventions/root.tsx#roottsx
export function Layout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <head>
        <meta charSet="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <Meta />
        <Links />
      </head>
      <body>
        {children}
        <ScrollRestoration />
        <Scripts />
      </body>
    </html>
  );
}

export default function App() {
  return <Outlet />;
}

export function ErrorBoundary({ error }: Route.ErrorBoundaryProps) {
  let message = "Oops!";
  let details = "An unexpected error occurred.";
  let stack: string | undefined;

  if (isRouteErrorResponse(error)) {
    message = error.status === 404 ? "404" : "Error";
    details =
      error.status === 404
        ? "The requested page could not be found."
        : error.statusText || details;
  } else if (import.meta.env.DEV && error && error instanceof Error) {
    details = error.message;
    stack = error.stack;
  }

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-md max-w-2xl">
        <h1 className="text-4xl font-bold text-red-600 mb-4">{message}</h1>
        <p className="text-gray-700 mb-4">{details}</p>
        {stack && (
          <pre className="bg-gray-100 p-4 rounded overflow-auto text-sm">
            {stack}
          </pre>
        )}
      </div>
    </div>
  );
}
