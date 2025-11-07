import type { RouteConfig } from "@react-router/dev/routes";

export default [
  {
    path: '/',
    file: './routes/layout.tsx',
    children: [
      {
        index: true,
        file: './routes/index.tsx',
      },
      {
        path: 'widgets',
        file: './routes/widgets.tsx',
      },
      {
        path: 'counter',
        file: './routes/counter.tsx',
      },
    ]
  }
] satisfies RouteConfig;
