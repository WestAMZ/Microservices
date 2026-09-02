import NextAuth, { Profile } from "next-auth"
import { OIDCConfig } from "next-auth/providers"
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6"
 
export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    // Configure the Duende Identity Server provider (matching Config.cs file in IdentityService)
    DuendeIDS6Provider({
        id: 'id-server',
        clientId: "nextApp", // The client ID you registered with the Duende Identity Server
        clientSecret: "secret", // The client secret you registered with the Duende Identity Server
        issuer: "http://localhost:5000", // The URL of your Duende Identity Server
        authorization: { params: { scope: 'openid profile auctionApp' } },
        idToken: true
    } as OIDCConfig<Profile>),
  ],
})