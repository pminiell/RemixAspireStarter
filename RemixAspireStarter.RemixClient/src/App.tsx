import { useLoaderData } from "react-router"
import type { WidgetSummary } from "./Models/WidgetSummary";

function App() {
  let widgets: WidgetSummary[] = useLoaderData();

  return (
    <div className="min-h-screen bg-gray-100 p-4">
      <h1 className="text-3xl font-bold text-center text-blue-600 mb-6">
        Widget List
      </h1>
      <ul className="space-y-4">
        {widgets.map((widget: WidgetSummary) => (
          <li
            key={widget.id}
            className="p-4 bg-white shadow-md rounded-lg border border-gray-200"
          >
            <span className="font-semibold">{widget.name}</span> - Quantity:{" "}
            <span className="text-gray-700">{widget.quantity}</span>
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App
