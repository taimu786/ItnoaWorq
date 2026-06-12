import type { ProfileDto } from "../../types/profile";

export default function AboutSection({ profile }: { profile: ProfileDto }) {
  return (
    <div className="bg-white p-4 rounded-lg shadow mb-4">
      <h3 className="font-semibold mb-2">About</h3>
      <p>{profile.summary || "No summary added."}</p>
    </div>
  );
}