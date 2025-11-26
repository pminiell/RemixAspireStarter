import { Form, Link, redirect } from "react-router";
import type { Route } from "../../../.react-router/types/src/routes/widgets/+types/widget";
import type { WidgetSummary } from "../../models/WidgetSummary";

export const meta: Route.MetaFunction = () => {
  return [
    { title: "Counter - Remix Aspire Starter" },
    { name: "description", content: "Interactive counter demonstration" },
  ];
};

export async function clientLoader({ params }: Route.ClientLoaderArgs) {
  const { id } = params;

  const response = await fetch(`/api/widgets/${id}`);
  if (!response.ok) {
    throw new Error(`Error fetching widget ${id}: ${response.statusText}`);
  }

  const data = await response.json();
  return data as WidgetSummary;
}

export async function clientAction({ request, params }: Route.ClientActionArgs) {
  const { id } = params;
  const formData = await request.formData();
  const name = formData.get("name");
  const quantity = formData.get("quantity");

  const response = await fetch(`/api/widgets`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ id, name, quantity }),
  });

  if (!response.ok) {
    throw new Error(`Error updating widget ${id}: ${response.statusText}`);
  }
  throw redirect('/widgets');
}

function WidgetDetails({ loaderData }: Route.ComponentProps) {
  const { name, quantity } = loaderData || { name: 'Unknown', quantity: 0 };


  return (

    <Form className="min-h-screen bg-gray-100 p-4" method="post">
      <h1 className="text-3xl font-bold text-center text-blue-600 mb-6">
        Widget Details
      </h1>
      <div className="max-w-2xl mx-auto bg-white p-8 rounded-lg shadow-md">
        <div className="mb-4">
          <label className="block text-gray-700 font-semibold mb-2" htmlFor="name">
            Name:
          </label>
          <input
            type="text"
            id="name"
            name="name"
            defaultValue={name}
            className="w-full px-3 py-2 border border-gray-300 rounded"
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700 font-semibold mb-2" htmlFor="quantity">
            Quantity:
          </label>
          <input
            type="number"
            id="quantity"
            name="quantity"
            defaultValue={quantity}
            className="w-full px-3 py-2 border border-gray-300 rounded"
          />
        </div>
        <button
          type="submit"
          className="px-4 py-2 bg-blue-500 text-white font-semibold rounded hover:bg-blue-600 cursor-pointer"
        >
          Save
        </button>
        <Link
          to="/widgets"
        >
          <button className="ml-2 px-4 py-2 bg-gray-500 text-white font-semibold rounded hover:bg-gray-600 cursor-pointer">
            Cancel
          </button>
        </Link>

      </div>
    </Form>
  )
}

export default WidgetDetails;

