import type { WidgetSummary } from "../models/WidgetSummary";
import type { Route } from "../../.react-router/types/src/routes/+types/widgets";

export const meta: Route.MetaFunction = () => {
  return [
    { title: "Widgets - Remix Aspire Starter" },
    { name: "description", content: "View all widgets from the API" },
  ];
};

// Client-side data loading
export async function clientLoader(): Promise<WidgetSummary[]> {
  const response = await fetch("/api/widgets");

  if (!response.ok) {
    throw new Error(`Error fetching widgets: ${response.statusText}`);
  }

  const data = await response.json();
  return data as WidgetSummary[];
}

function Widgets({ loaderData }: Route.ComponentProps) {
  const widgets = loaderData || [];
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
  );
}

export function ErrorBoundary({ error }: Route.ErrorBoundaryProps) {
  return (
    <div className="min-h-screen bg-gray-100 p-4">
      <div className="max-w-2xl mx-auto bg-white p-8 rounded-lg shadow-md">
        <h1 className="text-3xl font-bold text-red-600 mb-4">Error Loading Widgets</h1>
        <p className="text-gray-700">
          {error instanceof Error ? error.message : "Failed to load widgets from the API"}
        </p>
      </div>
    </div>
  );
}

export default Widgets;
