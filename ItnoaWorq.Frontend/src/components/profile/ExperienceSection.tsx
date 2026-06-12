import type { ExperienceDto } from "../../types/profile";

export default function ExperienceSection({
  experience,
}: {
  experience: ExperienceDto[];
}) {
  return (
    <div className="bg-white p-4 rounded-lg shadow mb-4">
      <h3 className="font-semibold mb-2">Experience</h3>

      {experience.map((exp) => (
        <div key={exp.id} className="mb-3">
          <p className="font-medium">{exp.title}</p>
          <p className="text-sm text-gray-500">{exp.companyName}</p>
        </div>
      ))}
    </div>
  );
}