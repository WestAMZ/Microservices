'use client'

import { Button } from 'flowbite-react'
import { signIn } from 'next-auth/react'


export const LoginButton = () => {
  return ( 
    // id-server is the id of the provider we configured in auth.ts
    // prompt: 'login' forces the user to enter their credentials even if they are already logged in allowing them to switch accounts if they want to
    <Button outline onClick={() => signIn('id-server', { redirectTo: '/' }, { prompt: 'login' })}>
        Login
    </Button>
  )
}
