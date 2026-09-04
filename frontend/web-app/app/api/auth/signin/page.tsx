import { EmptyFilter } from "@/app/components/EmptyFilter"

export default async function SignIn({ searchParams }:{ searchParams: { callbackUrl: string } }) {
    const { callbackUrl } = await searchParams;
    
    return (
        <EmptyFilter 
            title="You need to be logged in to access this page"
            subtitle="Please click below to login"
            showLogin={true}
            callbackUrl={ callbackUrl }
        />
    )
}
