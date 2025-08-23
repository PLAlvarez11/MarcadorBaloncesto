import { Component, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-scoreboard',
  standalone: true,
  templateUrl: './scoreboard.component.html',
  styleUrls: ['./scoreboard.component.scss']
})
export class ScoreboardComponent implements AfterViewInit {
  scoreA = 0;
  scoreB = 0;
  foulsA = 0;
  foulsB = 0;
  period = 1;
  posAActive = true;
  quarterSeconds = 600;
  remaining = this.quarterSeconds;
  timer: any = null;
  autoAdvance = false;
  autoPosLeader = false;
  audioCtx: AudioContext | null = null;

  ngAfterViewInit(): void {
    this.render();
    this.syncFab();
  }

  $(sel: string): HTMLElement {
    return document.querySelector(sel) as HTMLElement;
  }

  clamp0(v: number): number {
    return Math.max(0, v);
  }

  fmt(s: number): string {
    const m = Math.floor(s / 60).toString().padStart(2, '0');
    const ss = Math.floor(s % 60).toString().padStart(2, '0');
    return `${m}:${ss}`;
  }

  render(): void {
    this.$("#scoreA").textContent = String(this.scoreA);
    this.$("#scoreB").textContent = String(this.scoreB);
    this.$("#foulsA").textContent = String(this.foulsA);
    this.$("#foulsB").textContent = String(this.foulsB);
    this.$("#period").textContent = String(this.period);
    this.$("#time").textContent = this.fmt(this.remaining);
    this.$("#posA").style.opacity = this.posAActive ? "1" : ".25";
    this.$("#posB").style.opacity = this.posAActive ? ".25" : "1";
  }

  initAudio(): void {
    if (!this.audioCtx) this.audioCtx = new AudioContext();
  }

  beep(freq: number, attack: number, decay: number, type: OscillatorType, when = 0): void {
    if (!this.audioCtx) return;
    const ctx = this.audioCtx;
    const now = ctx.currentTime + when;
    const osc = ctx.createOscillator();
    const gain = ctx.createGain();
    osc.frequency.value = freq;
    osc.type = type;
    osc.connect(gain);
    gain.connect(ctx.destination);
    gain.gain.setValueAtTime(0, now);
    gain.gain.linearRampToValueAtTime(0.2, now + attack);
    gain.gain.exponentialRampToValueAtTime(0.001, now + attack + decay);
    osc.start(now);
    osc.stop(now + attack + decay);
  }

  playEndOfQuarter(): void {
    this.beep(1000, 0.18, 0.28, 'square', 0.00);
    this.beep(850, 0.18, 0.26, 'square', 0.22);
    this.beep(700, 0.35, 0.24, 'square', 0.44);
    this.showQuarterEndUI();
  }

  showQuarterEndUI(): void {
    this.$("#ovP").textContent = String(this.period);
    this.$("#ov").classList.add("show");
    this.$("#time").classList.add("flash");
    document.querySelector(".wrap")?.classList.add("ring");
    if (navigator.vibrate) navigator.vibrate([140, 80, 140]);
    setTimeout(() => {
      this.$("#ov").classList.remove("show");
      this.$("#time").classList.remove("flash");
      document.querySelector(".wrap")?.classList.remove("ring");
    }, 2500);
  }

  start(): void {
    this.initAudio();
    if (this.timer) return;
    this.timer = setInterval(() => {
      this.remaining = this.clamp0(this.remaining - 1);
      this.render();
      if (this.remaining === 0) {
        this.pause();
        this.playEndOfQuarter();
        if (this.autoAdvance) {
          this.period = Math.min(4, this.period + 1);
          this.resetClock();
        }
        if (this.autoPosLeader) {
          this.posAActive = this.scoreA >= this.scoreB;
        }
        this.render();
      }
    }, 1000);
  }

  pause(): void {
    clearInterval(this.timer);
    this.timer = null;
  }

  resetClock(): void {
    this.pause();
    this.remaining = this.quarterSeconds;
    this.render();
  }

  syncFab(): void {
    const controls = this.$("#controls");
    const fab = this.$("#fab");
    const update = () => {
      if (window.matchMedia("(max-width: 960px)").matches) {
        fab.style.display = "inline-flex";
      } else {
        fab.style.display = "none";
        controls.classList.remove("open");
      }
    };
    fab.onclick = () => controls.classList.toggle("open");
    window.addEventListener("resize", update);
    update();
  }
}
