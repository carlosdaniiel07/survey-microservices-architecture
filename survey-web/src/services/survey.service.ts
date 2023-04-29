import { Survey } from "../entities";
import http from "../infra/http";
import { SaveAnswerModel } from "../models";

const findAll = async (): Promise<Survey[]> => {
  const { surveys } = await http.get<{ surveys: Survey[] }>("/v1/surveys");
  return surveys;
};

const saveAnswer = async (
  surveyId: string,
  data: SaveAnswerModel
): Promise<void> => {
  await http.post<SaveAnswerModel>(`/v1/surveys/${surveyId}/answer`, data);
};

export const surveyService = {
  findAll,
  saveAnswer,
};

export default surveyService;
