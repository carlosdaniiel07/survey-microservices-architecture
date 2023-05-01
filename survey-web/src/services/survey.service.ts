import { gql } from "@apollo/client";
import { Survey } from "../entities";
import http from "../infra/http";
import graphQLClient from "../infra/graphql-client";
import { SaveAnswerModel } from "../models";

const findAll = async (): Promise<Survey[]> => {
  const { surveys } = await http.get<{ surveys: Survey[] }>("/v1/surveys");
  return surveys;
};

const findAllGraphQL = async (): Promise<Survey[]> => {
  const query = gql`
    query {
      allSurveys {
        surveys {
          id
          question
          availableAnswers
          isActive
          startAt
          endAt
        }
      }
    }
  `;
  const result = await graphQLClient.query({
    query,
  });

  return result.data.allSurveys.surveys ?? [];
};

const saveAnswer = async (
  surveyId: string,
  data: SaveAnswerModel
): Promise<void> => {
  await http.post<SaveAnswerModel>(`/v1/surveys/${surveyId}/answer`, data);
};

const saveAnswerGraphQL = async (
  surveyId: string,
  data: SaveAnswerModel
): Promise<void> => {
  await graphQLClient.mutate({
    mutation: gql`
      mutation ($surveyId: UUID!, $value: String!) {
        addSurveyAnswer(input: { surveyId: $surveyId, value: $value }) {
          id
        }
      }
    `,
    variables: {
      surveyId: surveyId,
      value: data.value,
    },
  });
};

export const surveyService = {
  findAll,
  findAllGraphQL,
  saveAnswer,
  saveAnswerGraphQL,
};

export default surveyService;
