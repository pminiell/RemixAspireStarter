import { useState } from "react";
import type { Route } from "../../.react-router/types/src/routes/+types/counter";

export const meta: Route.MetaFunction = () => {
  return [
    { title: "Counter - Remix Aspire Starter" },
    { name: "description", content: "Interactive counter demonstration" },
  ];
};

function Counter() {
  const [count, setCount] = useState(0);

  return (
    <div className="flex flex-col items-center justify-center bg-gray-100 p-6 rounded-lg shadow-md w-min mx-auto">
      <h1 className="text-2xl font-bold text-gray-800 mb-4">Counter</h1>
      <p className="text-xl text-gray-700 mb-6">Count: {count}</p>
      <div className="flex space-x-4">
        <button
          onClick={() => setCount(count + 1)}
          className="px-4 py-2 bg-blue-500 text-white font-semibold rounded hover:bg-blue-600 cursor-pointer"
        >
          Increment
        </button>
        <button
          onClick={() => setCount(count - 1)}
          className="px-4 py-2 bg-red-500 text-white font-semibold rounded hover:bg-red-600 cursor-pointer"
        >
          Decrement
        </button>
        <button
          onClick={() => setCount(0)}
          className="px-4 py-2 bg-gray-500 text-white font-semibold rounded hover:bg-gray-600 cursor-pointer"
        >
          Reset
        </button>
      </div>
    </div>
  );
}

export default Counter;

