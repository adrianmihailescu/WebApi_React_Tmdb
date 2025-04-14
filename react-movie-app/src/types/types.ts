export type Movie = {
    id: number;
    title: string;
    overview: string;
    comments?: Comment[];
  };

  export type Comment = {
    id: number;
    content: string;
    username: string;
    createdAt: string;
  };