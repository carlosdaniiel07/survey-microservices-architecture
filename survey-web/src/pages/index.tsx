import Head from "next/head";
import { useEffect, useState, useCallback } from "react";
import { toast } from "react-toastify";
import { Survey } from "../entities";
import { surveyService } from "../services";
import {
  SurveyCard,
  SurveyCardSkeleton,
  AnswerSurveyModal,
} from "../components";

type Status = "idle" | "loading" | "submitting";

export default function Home() {
  const [status, setStatus] = useState<Status>("loading");
  const [surveys, setSurveys] = useState<Survey[]>();
  const [showModal, setShowModal] = useState(false);
  const [survey, setSurvey] = useState<Survey | null>();

  useEffect(() => {
    const loadAllSurveys = () => {
      setStatus("loading");
      surveyService
        .findAllGraphQL()
        .then(setSurveys)
        .catch(() =>
          toast.error("Ocorreu um erro ao carregar a listagem de enquetes")
        )
        .finally(() => setStatus("idle"));
    };

    loadAllSurveys();
  }, []);

  const handleClickOnSurvey = useCallback((item: Survey) => {
    setSurvey(item);
    setShowModal(true);
  }, []);

  const handleSaveAnswer = useCallback(
    (answer: string) => {
      setStatus("submitting");
      surveyService
        .saveAnswerGraphQL(survey?.id as string, {
          value: answer,
        })
        .then(() => {
          toast.success("Resposta enviada com sucesso!");
          setSurvey(null);
          setShowModal(false);
        })
        .catch(() => toast.error("Ocorreu um erro ao enviar a resposta"))
        .finally(() => setStatus("idle"));
    },
    [survey?.id]
  );

  return (
    <>
      <Head>
        <title>Survey Web</title>
      </Head>
      <main className="min-h-screen flex bg-gradient-to-b from-indigo-900 via-indigo-600 to-indigo-700 pt-32 px-14 text-black">
        <section className="container mx-auto bg-white border-gray-600 border-t-2 border-x-2 rounded-t-2xl py-12 px-16">
          <div className="text-center">
            <span className="text-2xl">
              {status === "loading" ? (
                <div className="flex justify-center animate-pulse">
                  <div className="h-8 w-1/2 bg-slate-400 rounded"></div>
                </div>
              ) : (
                <span>
                  {(surveys?.length ?? 0) > 0
                    ? `${surveys?.length} enquete(s) disponíveis para responder!`
                    : "Nenhuma enquete disponível para responder :("}
                </span>
              )}
            </span>
          </div>

          <div className="mt-14 grid md:grid-cols-2 sm:grid-cols-1 gap-8">
            {status === "loading"
              ? [1, 2, 3, 4, 5, 6].map((_, index) => (
                  <SurveyCardSkeleton key={`survey-card-skeleton-${index}`} />
                ))
              : surveys?.map((item) => (
                  <SurveyCard
                    key={item.id}
                    survey={item}
                    onClick={() => handleClickOnSurvey(item)}
                  />
                ))}
          </div>

          <AnswerSurveyModal
            showModal={showModal}
            survey={survey}
            onClose={() => setShowModal(false)}
            onSubmit={(answer) => handleSaveAnswer(answer)}
          />
        </section>
      </main>
    </>
  );
}
