import { Button } from "flowbite-react"
import Link from "next/link"

export const UserActions = () => {
  return (
    <Button>
        <Link href="/session">Session</Link>
    </Button>
  )
}
