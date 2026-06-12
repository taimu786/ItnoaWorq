import type { EducationDto } from "../../types/profile";

export default function EducationSection({
  education,
}: {
  education: EducationDto[];
}) {
  return (
    <div className="bg-white p-4 rounded-lg shadow mb-4">
      <h3 className="font-semibold mb-2">Education</h3>

      {education.map((edu) => (
        <div key={edu.id} className="mb-3">
          <p className="font-medium">{edu.school}</p>
          <p className="text-sm text-gray-500">{edu.degree}</p>
        </div>
      ))}
    </div>
  );
}