import { ApolloClient, InMemoryCache } from "@apollo/client";

const apiUrl = String(process.env.NEXT_PUBLIC_API_URL).replace("/api", "");
const client = new ApolloClient({
  uri: `${apiUrl}/graphql`,
  cache: new InMemoryCache(),
});

export default client;
