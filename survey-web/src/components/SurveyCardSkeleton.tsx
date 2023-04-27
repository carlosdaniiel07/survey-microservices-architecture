type SurveyCardSkeletonProps = {};

export function SurveyCardSkeleton(props: SurveyCardSkeletonProps) {
  return (
    <div className="animate-pulse flex flex-col justify-between h-32 bg-slate-300 border-2 border-slate-300 rounded-lg p-4">
      <div className="h-4 bg-slate-400 rounded"></div>
      <div className="h-4 bg-slate-400 rounded"></div>
      <div className="h-4 w-1/3 self-end bg-slate-400 rounded"></div>
    </div>
  );
}
