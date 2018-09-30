import { Injectable } from "@angular/core";
import { Title, Meta } from "@angular/platform-browser";
import { StringUtils } from "src/app/utils/string.utils";

@Injectable()
export class SeoService{
    private titleService: Title;
    private meta: Meta;

    public constructor(titleService: Title, meta: Meta){
        this.titleService = titleService;
        this.meta = meta;
        this.setTitle('');
    }

    public setSeoData(seoModel: SeoModel){
        this.setTitle(seoModel.title);
        this.setMetaDescription(seoModel.description);
        this.setMetaKeywords(seoModel.keywords);
        this.setMetaRobots(seoModel.robots);
    }

    private setTitle(newTitle: string){
        if(StringUtils.isNullOrEmpty(newTitle)) {newTitle = "Defina um Título"}
        this.titleService.setTitle(newTitle + " - Eventos.IO")
    }

    private setMetaDescription(description: string) {        
        if (StringUtils.isNullOrEmpty(description)) 
            description = "Aqui você encontra um evento técnico próximo de você";
        this.getOrCreateMetaElement('description', description);
    }

    private setMetaKeywords(keywords: string) {
        if (StringUtils.isNullOrEmpty(keywords)) 
        keywords = "eventos,workshops,encontros,congressos,comunidades,tecnologia";
        this.getOrCreateMetaElement('keywords', keywords);
    }

    private setMetaRobots(robots: string) {
        if (StringUtils.isNullOrEmpty(robots)) 
        robots = "all";
        this.getOrCreateMetaElement('robots', robots);        
    }

    private getOrCreateMetaElement(names: string, contents: string) {        
        var metaElements = this.meta.getTag('name=' + names);
        if(metaElements === null)
            this.meta.addTag({ name: names, content: contents });
        else
            this.meta.updateTag({ name: names, content: contents });
    }
}

export class SeoModel{
    public title: string = '';
    public description: string = '';
    public robots: string = '';
    public keywords: string = '';
}