import { BaseEntity } from "./base.entity";

export type Survey = BaseEntity & {
  question: string;
  availableAnswers: string[];
  startAt: Date | string;
  endAt: Date | string;
  isActive: boolean;
};
