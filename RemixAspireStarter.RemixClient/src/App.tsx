import { useLoaderData } from "react-router"
import type { WidgetSummary } from "./Models/WidgetSummary";

function App() {
  let widgets: WidgetSummary[] = useLoaderData();
  return (
    <div>
      <h1>Widget List</h1>
      <ul>
        {widgets.map((widget: WidgetSummary) => (
          <li key={widget.id}>
            {widget.name} - Quantity: {widget.quantity}
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App
