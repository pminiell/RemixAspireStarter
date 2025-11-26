import type { WidgetSummary } from "../models/WidgetSummary";
import type { Route } from "../../.react-router/types/src/routes/+types/widgets";
import { Link } from "react-router";
import { useState } from "react";

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
  const widgets: WidgetSummary[] = loaderData || [];
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [widgetToDelete, setWidgetToDelete] = useState<string | null>(null);


  const openModal = (widgetId: string) => {
    setWidgetToDelete(widgetId);
    setIsModalOpen(true);
  }
  const closeModal = () => {
    setWidgetToDelete(null);
    setIsModalOpen(false);
  }

  const confirmDelete = async () => {
    if (!widgetToDelete) return;

    const response = await fetch(`/api/widgets/${widgetToDelete}`, {
      method: "DELETE",
    });

    if (!response.ok) {
      alert(`Error deleting widget: ${response.statusText}`);
    } else {
      // Optionally, you can refresh the page or update the state to reflect the deletion
      window.location.reload();
    }
    closeModal();
  }



  return (
    <div className="min-h-screen bg-gray-100 p-4">
      <h1 className="text-3xl font-bold text-center text-blue-600 mb-6">
        Widget List
      </h1>
      <Link to="/widgets/new" className="mb-4 inline-block px-4 py-2 bg-green-500 text-white font-semibold rounded hover:bg-green-600 cursor-pointer">
        Create New Widget
      </Link>
      <ul className="space-y-4">
        {widgets.length === 0 ? (
          <li className="text-gray-700">No widgets available.</li>
        ) :
          widgets.map((widget: WidgetSummary) => (
            <li
              key={widget.id}
              className="p-4 bg-white shadow-md rounded-lg border border-gray-200"
            >
              <div className="flex flex-row">
                <div className="flex-grow m-2">
                  <span className="font-semibold">{widget.name}</span> - Quantity:{" "}
                  <span className="text-gray-700">{widget.quantity}</span>
                </div>
                <div>
                  <Link to={`/widgets/${widget.id}`}><button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded hover:bg-blue-600 cursor-pointer">Details</button></Link>
                  <button className="ml-2 px-4 py-2 bg-red-500 text-white font-semibold rounded hover:bg-red-600 cursor-pointer" onClick={() => openModal(widget.id)}>Delete</button>
                </div>
              </div>
            </li>
          ))}
      </ul>

      {isModalOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
          <div className="bg-white p-6 rounded shadow-md">
            <h2 className="text-xl font-bold mb-4">Confirm Deletion</h2>
            <p>Are you sure you want to delete this widget?</p>
            <div className="mt-4 flex justify-end space-x-2">
              <button
                className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
                onClick={closeModal}
              >
                Cancel
              </button>
              <button
                className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
                onClick={confirmDelete}
              >
                Delete
              </button>
            </div>
          </div>
        </div>
      )}

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
