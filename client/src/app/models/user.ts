import { Role } from "./role";

export interface User {
    username: string;
    token: string;
    email: string;
    roles: Role[];
  }
  