import { ComponentFixture, TestBed } from '@angular/core/testing';
import { signal, WritableSignal } from '@angular/core';
import { LeaderboardEntry } from '../../app/core/models/leaderboard-entry';
import { LeaderboardService } from '../../app/core/services/leaderboard.service';
import { LeaderboardActiveComponent } from './leaderboard-active.component';

describe('LeaderboardActiveComponent', () => {
  let fixture: ComponentFixture<LeaderboardActiveComponent>;
  let leaderboardEntries: WritableSignal<LeaderboardEntry[]>;
  let loadLeaderboardEntries: jasmine.Spy;

  const entries: LeaderboardEntry[] = [
    { position: 1, playerName: 'Alice', gamesPlayed: 4, totalScore: 120 },
    { position: 2, playerName: 'Bob', gamesPlayed: 3, totalScore: 95 },
    { position: 3, playerName: 'Cara', gamesPlayed: 2, totalScore: 110 }
  ];

  beforeEach(async () => {
    leaderboardEntries = signal<LeaderboardEntry[]>([]);
    loadLeaderboardEntries = jasmine.createSpy('loadLeaderboardEntries')
      .and.callFake(() => leaderboardEntries.set(entries));

    await TestBed.configureTestingModule({
      imports: [LeaderboardActiveComponent],
      providers: [{
        provide: LeaderboardService,
        useValue: { leaderboardEntries, loadLeaderboardEntries }
      }]
    }).compileComponents();

    fixture = TestBed.createComponent(LeaderboardActiveComponent);
    fixture.detectChanges();
  });

  it('loads leaderboard entries and renders the table rows from the signal', () => {
    expect(loadLeaderboardEntries).toHaveBeenCalled();
    expect(rowTexts()).toEqual([
      '1. Alice 4 120',
      '2. Bob 3 95',
      '3. Cara 2 110'
    ]);
  });

  it('updates rendered rows when the leaderboard signal changes', () => {
    leaderboardEntries.set([
      { position: 1, playerName: 'Dev', gamesPlayed: 1, totalScore: 42 }
    ]);
    fixture.detectChanges();

    expect(rowTexts()).toEqual(['1. Dev 1 42']);
  });

  it('sorts by position in ascending and descending order', () => {
    clickSortHeader('Position');
    expect(columnTexts(1)).toEqual(['1.', '2.', '3.']);

    clickSortHeader('Position');
    expect(columnTexts(1)).toEqual(['3.', '2.', '1.']);
  });

  it('sorts player names in ascending and descending order', () => {
    clickSortHeader('Player Name');
    expect(columnTexts(2)).toEqual(['Alice', 'Bob', 'Cara']);

    clickSortHeader('Player Name');
    expect(columnTexts(2)).toEqual(['Cara', 'Bob', 'Alice']);
  });

  it('sorts scores numerically in ascending and descending order', () => {
    clickSortHeader('Total Score');
    expect(columnTexts(4)).toEqual(['95', '110', '120']);

    clickSortHeader('Total Score');
    expect(columnTexts(4)).toEqual(['120', '110', '95']);
  });

  it('sorts games played in ascending and descending order', () => {
    clickSortHeader('Games Played');
    expect(columnTexts(3)).toEqual(['2', '3', '4']);

    clickSortHeader('Games Played');
    expect(columnTexts(3)).toEqual(['4', '3', '2']);
  });

  it('filters player names case-insensitively and ignores surrounding whitespace', () => {
    const root = fixture.nativeElement as HTMLElement;
    const input = root.querySelector(
      'input[type="search"]'
    ) as HTMLInputElement;

    input.value = '  aL  ';
    input.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    expect(rowTexts()).toEqual(['1. Alice 4 120']);
  });

  it('renders the player data as JSON on each row', () => {
    const root = fixture.nativeElement as HTMLElement;
    const row = root.querySelector(
      'tr.mat-mdc-row'
    ) as HTMLTableRowElement;

    expect(row.getAttribute('data-player')).toBe(
      '{"name":"Alice","score":120,"index":0}'
    );
  });

  function clickSortHeader(label: string): void {
    const root = fixture.nativeElement as HTMLElement;
    const header = Array.from(
      root.querySelectorAll<HTMLElement>('th[mat-sort-header]')
    ).find(element => element.textContent?.trim() === label) as HTMLElement;

    header.click();
    fixture.detectChanges();
  }

  function rowTexts(): string[] {
    const root = fixture.nativeElement as HTMLElement;
    return Array.from(
      root.querySelectorAll<HTMLElement>('tr.mat-mdc-row')
    ).map(row =>
      Array.from(row.querySelectorAll<HTMLElement>('td'))
        .map(cell => cell.textContent?.trim() ?? '')
        .join(' ')
    );
  }

  function columnTexts(column: number): string[] {
    const root = fixture.nativeElement as HTMLElement;
    return Array.from(
      root.querySelectorAll<HTMLElement>(
        `tr.mat-mdc-row td:nth-child(${column})`
      )
    ).map(cell => cell.textContent?.trim() ?? '');
  }
});
