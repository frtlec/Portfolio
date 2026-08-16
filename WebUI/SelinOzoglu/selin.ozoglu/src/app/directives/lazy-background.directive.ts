import { Directive, ElementRef, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';

// CSS background-image, <img>'nin aksine tarayicinin native loading="lazy"
// ozelligini kullanamaz. Bu directive ayni gorsel/CSS/hover-zoom davranisini
// birebir koruyarak (sadece background-image ne zaman set edildigini
// erteleyerek) grid karolarina lazy-load kazandirir.
@Directive({
  selector: '[appLazyBg]',
  standalone: false
})
export class LazyBackgroundDirective implements OnInit, OnChanges, OnDestroy {
  @Input() appLazyBg: string;

  private observer: IntersectionObserver;
  private applied = false;

  constructor(private el: ElementRef<HTMLElement>) { }

  ngOnInit(): void {
    if (typeof IntersectionObserver === 'undefined') {
      // Cok eski taraycilarda gozlemci yoksa dogrudan yukle, sayfa hic kirilmasin.
      this.apply();
      return;
    }

    this.observer = new IntersectionObserver(entries => {
      if (entries.some(entry => entry.isIntersecting)) {
        this.apply();
        this.observer.disconnect();
      }
    }, { rootMargin: '200px' });

    this.observer.observe(this.el.nativeElement);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.applied && changes['appLazyBg'] && !changes['appLazyBg'].firstChange) {
      this.apply();
    }
  }

  ngOnDestroy(): void {
    this.observer?.disconnect();
  }

  private apply(): void {
    if (this.appLazyBg) {
      this.el.nativeElement.style.backgroundImage = `url(${this.appLazyBg})`;
      this.applied = true;
    }
  }
}
