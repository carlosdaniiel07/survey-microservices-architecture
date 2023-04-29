import { Survey } from "../entities";

type SurveyCardProps = {
  survey: Survey;
  onClick(): any;
};

export function SurveyCard({ survey, onClick }: SurveyCardProps) {
  return (
    <div
      onClick={() => onClick()}
      className="
                flex
                flex-col
                justify-between
                h-32
                bg-zinc-50
                border-2
                border-zinc-100
                rounded-lg
                p-4
                font-medium
                transition
                ease-in-out
                duration-300
                hover:-translate-y-3
                cursor-pointer"
    >
      <p className="text-lg">{survey.question}</p>

      <div className="flex items-center justify-end space-x-1.5">
        <span className="relative flex h-3 w-3">
          <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
          <span className="relative inline-flex rounded-full h-3 w-3 bg-green-500"></span>
        </span>
        {survey.isActive ? <span className="text-green-600">Ativa</span> : null}
      </div>
    </div>
  );
}
