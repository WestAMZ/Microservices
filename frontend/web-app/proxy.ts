export { auth as proxy } from "@/auth"

export const config = {
    matcher: [
        '/session'
    ],
    pages: {
        signIn: '/api/auth/signin',
    }
}