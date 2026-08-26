import Image from "next/image"

type Props = {
    auction: any
}
export const AuctionCard = ({auction}: Props) => {
  return (
    <div>
        <a href="#">
            <div className="relative w-full bg-gray-200 aspect-video rounded-lg overflow-hidden">
                <Image
                    src={auction.imageUrl}
                    alt="Image of car"
                    fill
                    className="object-cover"
                />
            </div>
        </a>
    </div>
  )
}
