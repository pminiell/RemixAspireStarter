import type { WidgetSummary } from './models/WidgetSummary';


export async function getWidgetsAsync(): Promise<WidgetSummary[]> {
  const response = await fetch("/widgets");

  if (!response.ok) {
    throw new Error(`Error fetching widgets: ${response.statusText}`);
  }

  const data = await response.json();
  return data as WidgetSummary[];
}


