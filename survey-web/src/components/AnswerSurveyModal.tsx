import { useState, useCallback, useMemo } from "react";
import { Survey } from "../entities";
import { Modal } from "./Modal";

type AnswerSurveyModalProps = {
  showModal: boolean;
  survey?: Survey | null;
  onClose(): void;
  onSubmit(answer: string): void | Promise<void>;
};

export function AnswerSurveyModal({
  showModal,
  survey,
  onClose,
  onSubmit,
}: AnswerSurveyModalProps) {
  const [selectedAnswer, setSelectedAnswer] = useState<string | null>();

  const handleSelectAnswer = useCallback(
    (answer: string) => {
      setSelectedAnswer(answer === selectedAnswer ? null : answer);
    },
    [selectedAnswer]
  );

  const handleSubmit = useCallback(() => {
    onSubmit(selectedAnswer as string);
    setSelectedAnswer(null);
  }, [onSubmit, selectedAnswer]);

  const isValidAnswer = useMemo<boolean>(() => {
    return survey?.availableAnswers.includes(selectedAnswer ?? "") ?? false;
  }, [selectedAnswer, survey?.availableAnswers]);

  return (
    <Modal showModal={showModal}>
      <div className="h-[42rem] overflow-auto relative flex-auto p-6">
        <p className="font-medium text-center text-lg">{survey?.question}</p>
        <div className="flex flex-col items-center mt-6 space-y-3">
          {survey?.availableAnswers.map((answer, index) => (
            <button
              key={`answer-${index}`}
              className={`min-w-[10rem] border text-sm border-indigo-600 rounded-md uppercase py-2 px-4 hover:bg-indigo-600 hover:text-white transition duration-300 outline-none ${
                answer === selectedAnswer
                  ? "text-white bg-indigo-600"
                  : "text-indigo-600"
              }`}
              type="button"
              onClick={() => handleSelectAnswer(answer)}
            >
              {answer}
            </button>
          ))}
        </div>
      </div>
      <div className="flex items-center justify-end p-4 space-x-3 border-t border-solid border-slate-200 rounded-b">
        <button
          className="border text-sm border-indigo-600 rounded-md uppercase text-indigo-600 py-2 px-4 hover:bg-indigo-600 hover:text-white transition duration-300 outline-none"
          type="button"
          onClick={onClose}
        >
          Cancelar
        </button>
        <button
          className="bg-indigo-600 text-white border text-sm rounded-md uppercase py-2 px-4 transition duration-300 outline-none disabled:opacity-30"
          type="button"
          disabled={!isValidAnswer}
          onClick={handleSubmit}
        >
          Enviar
        </button>
      </div>
    </Modal>
  );
}
