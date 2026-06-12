import type { ProfileDto } from "../../types/profile";

export default function ProfileHeader({ profile }: { profile: ProfileDto }) {
  return (
    <div className="bg-white rounded-lg shadow mb-4">

      <div className="h-40 bg-gradient-to-r from-indigo-500 to-purple-600 rounded-t-lg" />

      <div className="p-4 relative">
        <div className="w-24 h-24 bg-gray-300 rounded-full border-4 border-white absolute -top-12" />

        <div className="mt-12">
          <h2 className="text-xl font-bold">{profile.headline}</h2>
          <p className="text-gray-600">{profile.profession}</p>
          <p className="text-gray-500 text-sm">{profile.location}</p>
        </div>
      </div>
    </div>
  );
}