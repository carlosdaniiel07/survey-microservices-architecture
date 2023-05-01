import http from "k6/http";
import { sleep } from "k6";

export const options = {
  stages: [
    { duration: "3m", target: 50 },
  ],
};

const API_URL = "<API URL>";
const SURVEY_ID = "<SURVEY ID>";

const availableValues = [
  "C#",
  "Java",
  "JavaScript",
  "TypeScript",
  "Kotlin",
  "PHP",
  "Go",
  "Ruby",
  "Rust",
  "Scala",
  "Elixir",
  "Clojure",
  "VB.NET",
];

export default () => {
  http.post(
    `${API_URL}/v1/surveys/${SURVEY_ID}/answer`,
    JSON.stringify({
      value:
        availableValues[Math.floor(Math.random() * availableValues.length)],
    }),
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
  sleep(1);
};
