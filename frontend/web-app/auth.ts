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
    } as OIDCConfig<Omit<Profile, 'username'>>),
  ],
  // Callbacks allow you to control what happens when a user signs in, signs out, or when a session is created or updated. 
  // You can use these callbacks to customize the behavior of your authentication flow.
  callbacks:{
    async authorized({ auth }){
      return !!auth;
    },
    async jwt({ token, profile }) {
      
      if(profile){
        token.username = profile.username;
      }
      return token;
    },
    
    async session({ session, token}){
      if(token){
        session.user.username = token.username;
      }
      return session;
    }
  }
})