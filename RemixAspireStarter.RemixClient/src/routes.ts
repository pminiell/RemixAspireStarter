import { type RouteConfig, layout, index, route } from "@react-router/dev/routes";

export default [
  layout("./routes/layout.tsx", [
    index("./routes/index.tsx"),
    route('widgets', './routes/widgets.tsx'),
    route('counter', './routes/counter.tsx'),
  ])
] satisfies RouteConfig;
