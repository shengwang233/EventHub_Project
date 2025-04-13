"use server";
import { handleError } from "../utils";
import { connectToDatabase } from "../database";
import User from "../database/models/user.model";
import Event from "../database/models/event.model";
import { revalidatePath } from "next/cache";
import {
  CreateEventParams,
  UpdateEventParams,
  DeleteEventParams,
  GetAllEventsParams,
  GetEventsByUserParams,
  GetRelatedEventsByCategoryParams,
} from "@/types";
const populateEvent = (query: any) => {
  return query.populate("organizerId", "_id firstName lastName");
};

export const createEvent = async ({ event }: CreateEventParams) => {
  try {
    const res = await fetch("http://localhost:5000/api/events", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(event),
    });

    if (!res.ok) throw new Error("Failed to create event");

    return await res.json();
  } catch (error) {
    handleError(error);
  }
};

export const getEventById = async (eventId: string) => {
  try {
    const res = await fetch(`http://localhost:5000/api/events/${eventId}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
      cache: "no-store",
    });

    if (!res.ok) return null;

    return await res.json();
  } catch (error) {
    console.error("Failed to fetch event:", error);
    return null;
  }
};

export async function getAllEvents(query?: string) {
  try {
    const url = new URL("http://localhost:5000/api/events");
    if (query) url.searchParams.append("query", query);

    const res = await fetch(url.toString(), {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
      cache: "no-store",
    });

    if (!res.ok) throw new Error("Failed to fetch events");

    const events = await res.json(); // List<Event>
    return events;
  } catch (error) {
    handleError(error);
    return [];
  }
}

export const deleteEvent = async ({ eventId, path }: DeleteEventParams) => {
  try {
    await connectToDatabase();
    const deletedEvent = await Event.findByIdAndDelete(eventId);
    if (deletedEvent) revalidatePath(path);
    return JSON.parse(JSON.stringify(event));
  } catch (error) {
    console.log(error);
  }
};

export async function updateEvent({ event, path }: UpdateEventParams) {
  try {
    const res = await fetch(`http://localhost:5000/api/events/${event.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(event),
    });

    if (!res.ok) throw new Error("Failed to update event");

    return await res.json();
  } catch (error) {
    handleError(error);
  }
}
