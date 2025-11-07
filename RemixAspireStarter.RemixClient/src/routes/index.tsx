function Index() {
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-50 p-6">
      <div className="bg-white shadow-md rounded-lg p-8 max-w-2xl text-center">
        <h1 className="text-3xl font-bold text-blue-600 mb-4">Welcome to Aspire!</h1>
        <p className="text-gray-700 text-lg mb-6">
          This application is built to demonstrate the power of modern web development practices. It combines a minimal API backend with CQRS patterns, a React Router-based client, and a PostgreSQL database for data persistence.
        </p>
        <h2 className="text-2xl font-semibold text-gray-800 mb-4">Why Aspire?</h2>
        <ul className="list-disc list-inside text-left text-gray-700 space-y-2">
          <li>Leverages <strong>Minimal APIs</strong> for lightweight and fast backend development.</li>
          <li>Implements <strong>CQRS</strong> (Command Query Responsibility Segregation) for clean and scalable architecture.</li>
          <li>Uses <strong>React Router</strong> for seamless client-side navigation.</li>
          <li>Interacts with a <strong>PostgreSQL</strong> database for reliable and efficient data storage.</li>
        </ul>
        <p className="text-gray-700 text-lg mt-6">
          Aspire is designed to be a starting point for building modern, scalable, and maintainable web applications.
        </p>
      </div>
    </div>
  );
}

export default Index;

