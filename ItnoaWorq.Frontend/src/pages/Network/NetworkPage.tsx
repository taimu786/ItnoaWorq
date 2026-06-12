import MainLayout from "../../components/Layouts/MainLayout";

export default function NetworkPage() {
  return (
    <MainLayout>

      {/* CREATE POST */}
      <div className="bg-white p-4 rounded-lg shadow mb-4">
        <input
          placeholder="Start a post..."
          className="w-full p-2 border rounded-full"
        />
      </div>

      {/* POSTS */}
      <div className="bg-white p-4 rounded-lg shadow mb-4">
        <h3 className="font-semibold">User Name</h3>
        <p className="text-sm text-gray-500">2h ago</p>
        <p className="mt-2">This is a sample post</p>
      </div>

    </MainLayout>
  );
}